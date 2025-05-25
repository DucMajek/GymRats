import React, { useState, useEffect } from 'react';
import { useLocation, NavLink, useNavigate } from 'react-router-dom';
import {
  CDBSidebar,
  CDBSidebarHeader,
  CDBSidebarContent,
  CDBSidebarMenu,
  CDBSidebarMenuItem,
  CDBSidebarFooter
} from 'cdbreact';
import { useAuth } from './AuthContext';
import axios from 'axios';

import '../assets/styles/Sidebar.css';
import 'bootstrap/dist/css/bootstrap.min.css';

export default function Sidebar() {
  const [personalData, setPersonalData] = useState(null);
  const [error, setError] = useState(null);
  const [open, setOpen] = useState(false);
  const location = useLocation();
  const navigate = useNavigate();
  const { email, token, logout } = useAuth();

  useEffect(() => {
    const fetchData = async () => {
      if (!email || !token) return;

      try {
        const response = await axios.get(
          `https://localhost:44380/user/personal-data/${encodeURIComponent(email)}`,
          { headers: { Authorization: `Bearer ${token}` } }
        );
        setPersonalData(response.data);
      } catch (error) {
        console.error('Błąd pobierania danych:', error);
        setError(error.response?.data?.message || error.message);
        if (error.response?.status === 401) logout();
      }
    };

    fetchData();
  }, [location, email, token, logout]);

  const handleLogout = () => {
    logout();
    navigate('/');  // Tutaj dodajemy przekierowanie
  };

  return (
    <>
      <button className="sidebar-toggle" onClick={() => setOpen(o => !o)}>
        <i className="fa fa-bars" />
      </button>

      <div className={`sidebar-wrapper${open ? ' open' : ''}`}>
        <CDBSidebar className="app-sidebar">
          <CDBSidebarHeader prefix={<i className="fa fa-bars" />}>
            {personalData ? (
              `${personalData.name} ${personalData.surname}`
            ) : (
              <span className="text-warning">{error || 'Ładowanie...'}</span>
            )}
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
                <CDBSidebarMenuItem icon="calendar-alt">Zajęcia grupowe</CDBSidebarMenuItem>
              </NavLink>
            </CDBSidebarMenu>
          </CDBSidebarContent>

          <CDBSidebarFooter className="app-sidebar-footer">
            <NavLink exact to="/" onClick={handleLogout}>
              <CDBSidebarMenuItem icon="door-open">Wyloguj się</CDBSidebarMenuItem>
            </NavLink>
          </CDBSidebarFooter>
        </CDBSidebar>
      </div>
    </>
  );
}