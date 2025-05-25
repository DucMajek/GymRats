import { useState, useEffect } from 'react';
import axios from 'axios';


const api = axios.create({
  baseURL: 'https://localhost:44380',
});


export default function useGroupClasses() {
  const [classes, setClasses]     = useState([]);
  const [loading, setLoading]     = useState(true);
  const [error, setError]         = useState(null);
  const [signedIn, setSignedIn]   = useState(new Set());


  let email = '';
  try {
    const token = localStorage.getItem('token');
    if (token) {
      const p = JSON.parse(atob(token.split('.')[1]));
      email = p.email;
    }
  } catch {}

  useEffect(() => {
    (async () => {
      try {
        /* 1) load raw group classes */
        const { data: raw } = await api.get('/user/groupClasses');

        /* 2) load coach names */
        const enriched = await Promise.all(
          raw.map(async (cls) => {
            try {
              const { data: person } = await api.get(`/coaches/${cls.idCoach}`);
              return { ...cls, coachName: `${person.name} ${person.surname}` };
            } catch {
              return { ...cls, coachName: `Coach #${cls.idCoach}` };
            }
          })
        );
        setClasses(enriched);

        /* 3) load this users existing sign‑ups */
        if (email) {
          try {
            const { data: participation } = await api.get(
              `/user/participationInClass/${encodeURIComponent(email)}`
            );
            /* participation is array of ParticipationInClass { idParticipation, idGroup, idUser, ... }*/
            const signedSet = new Set(participation.map((p) => p.idGroup));
            setSignedIn(signedSet);
          } catch (e) {
            if (e.response?.status !== 404) console.error(e);
          }
        }
      } catch (e) {
        setError(e);
      } finally {
        setLoading(false);
      }
    })();
  }, [email]);

  /**
   * Sign in the user to a group.
   * Alerts if already signed (404) or on error.
   */
  const signIn = async (groupId) => {
    if (!email) {
      alert('You must be logged in');
      return;
    }
    try {
      await api.post(`/user/signInToGroup/${encodeURIComponent(email)}/${groupId}`);
      setSignedIn((prev) => new Set(prev).add(groupId));
    } catch (e) {
      if (e.response?.status === 404) {
        alert('You are already signed up for this group');
        /* marked as sign in */
        setSignedIn((prev) => new Set(prev).add(groupId));
      } else {
        console.error(e);
        alert('Failed to sign in: ' + (e.response?.data || e.message));
      }
    }
  };

  return { classes, loading, error, signedIn, signIn };
}
