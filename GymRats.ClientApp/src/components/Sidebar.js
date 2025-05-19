import React, { useState, useEffect } from 'react';
import {
  CDBSidebar,
  CDBSidebarHeader,
  CDBSidebarContent,
  CDBSidebarMenu,
  CDBSidebarMenuItem,
  CDBSidebarFooter
} from 'cdbreact';
import { NavLink } from 'react-router-dom';
import '../assets/styles/Sidebar.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';

export default function Sidebar() {
  const [personalData, setPersonalData] = useState(null);
  const [error, setError]        = useState(null);
  const [open, setOpen]         = useState(false);


  useEffect(() => {
    (async () => {
      const token = localStorage.getItem('token');
      if (!token) {
        setError('Brak tokena');
        return;
      }
      try {
        const payload = JSON.parse(atob(token.split('.')[1]));
        const email = payload.email;
        const res = await axios.get(
          `https://localhost:44380/user/personal-data/${encodeURIComponent(email)}`,
          { withCredentials: true }
        );
        setPersonalData(res.data);
      } catch (err) {
        if (err.response?.status === 404) {
          setError('Dane osobowe nie znalezione.');
        } else {
          setError('Coś poszło nie tak: ' + err.message);
        }
      }
    })();
  }, []);

  return (
    <>
      {/* Mobile hamburger */}
      <button
        className="sidebar-toggle"
        onClick={() => setOpen(o => !o)}
      >
        <i className="fa fa-bars" />
      </button>

      {/* Sidebar full-screen on mobile, fixed on desktop */}
      <div className={`sidebar-wrapper${open ? ' open' : ''}`}>
        <CDBSidebar className="app-sidebar">
          <CDBSidebarHeader prefix={<i className="fa fa-bars" />}>
            {error 
              ? <span className="text-danger">{error}</span>
              : personalData 
                ? `${personalData.name} ${personalData.surname}`
                : null}
          </CDBSidebarHeader>

          <CDBSidebarContent>
            <CDBSidebarMenu>
              <NavLink exact to="/user-profile">
                <CDBSidebarMenuItem icon="user">Moje konto</CDBSidebarMenuItem>
              </NavLink>
              <NavLink exact to="/gym-pass">
                <CDBSidebarMenuItem icon="ticket-alt">Karnety</CDBSidebarMenuItem>
              </NavLink>
              <NavLink exact to="/diets">
                <CDBSidebarMenuItem icon="carrot">Diety</CDBSidebarMenuItem>
              </NavLink>
              <NavLink exact to="/training-plans">
                <CDBSidebarMenuItem icon="clipboard-list">Plany treningowe</CDBSidebarMenuItem>
              </NavLink>
              <NavLink exact to="/courses">
                <CDBSidebarMenuItem icon="graduation-cap">Kursy</CDBSidebarMenuItem>
              </NavLink>
              <NavLink exact to="/Groupclass">
                <CDBSidebarMenuItem icon="calendar-alt">Groupclass</CDBSidebarMenuItem>
              </NavLink>
            </CDBSidebarMenu>
          </CDBSidebarContent>

          <CDBSidebarFooter className="app-sidebar-footer">
            <NavLink exact to="/" reloadDocument>
              <CDBSidebarMenuItem icon="door-open">Wyloguj się</CDBSidebarMenuItem>
            </NavLink>
          </CDBSidebarFooter>
        </CDBSidebar>
      </div>
    </>
  );
}
