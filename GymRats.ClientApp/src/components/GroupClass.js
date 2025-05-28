import { useState, useEffect, useCallback } from 'react';
import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:44380',
});

export default function useGroupClasses() {
  const [classes, setClasses]   = useState([]);
  const [loading, setLoading]   = useState(true);
  const [error, setError]       = useState(null);
  const [signedIn, setSignedIn] = useState(new Set());

  // wydobycie e-maila z tokena
  let email = '';
  try {
    const token = localStorage.getItem('token');
    if (token) {
      const payload = JSON.parse(atob(token.split('.')[1]));
      email = payload.email;
    }
  } catch (err) {
    console.error('Token parse error:', err);
  }

  useEffect(() => {
    (async () => {
      setLoading(true);
      try {
        const { data: raw } = await api.get('/user/groupClasses');

        const enriched = await Promise.all(
          raw.map(async (cls) => {
            try {
              const { data: coach } = await api.get(`/coaches/${cls.idCoach}`);
              return { ...cls, coachName: `${coach.name} ${coach.surname}` };
            } catch {
              return { ...cls, coachName: `Coach #${cls.idCoach}` };
            }
          })
        );
        setClasses(enriched);

        if (email) {
          try {
            const { data: participation } = await api.get(
              `/user/participationInClass/${encodeURIComponent(email)}`
            );
            setSignedIn(new Set(participation.map(p => p.idGroup)));
          } catch (err) {
            if (err.response?.status !== 404) console.error(err);
          }
        }
      } catch (err) {
        console.error(err);
        setError(err);
      } finally {
        setLoading(false);
      }
    })();
  }, [email]);

  const signIn = useCallback(async (groupId) => {
    if (!email) {
      alert('Musisz być zalogowany');
      return;
    }
    try {
      await api.post(`/user/signInToGroup/${encodeURIComponent(email)}/${groupId}`);
      setSignedIn(prev => new Set(prev).add(groupId));
      
    } catch (err) {
      console.error(err);
      if (err.response?.status === 404) {
        alert('Jesteś już zapisany na tę grupę');
        setSignedIn(prev => new Set(prev).add(groupId));
      } else {
        alert('Błąd zapisu: ' + (err.response?.data || err.message));
      }
    }
  }, [email]);

  const drop = useCallback(async (groupId) => {
    if (!email) {
      alert('Musisz być zalogowany');
      return;
    }
    try {
      await api.delete(`/user/dropClass/${encodeURIComponent(email)}/${groupId}`);
      setSignedIn(prev => {
        const clone = new Set(prev);
        clone.delete(groupId);
        return clone;
      });
      
    } catch (err) {
      console.error(err);
      if (err.response?.status === 404) {
        alert('Nie masz rezerwacji na tę grupę');
      } else {
        alert('Błąd rezygnacji: ' + (err.response?.data || err.message));
      }
    }
  }, [email]);

  return { classes, loading, error, signedIn, signIn, drop };
}
