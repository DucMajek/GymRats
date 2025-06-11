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

function Sidebar() {
  const [personalData, setPersonalData] = useState(null);
  const [error, setError] = useState(null);
  const [open, setOpen] = useState(false);
  const location = useLocation();
  const navigate = useNavigate();
  const { email, token, logout } = useAuth();
  const [personalOpen, setPersonalOpen] = useState(false);
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
    navigate('/', { replace: true }); 
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
              <NavLink to="/user-profile">
                {({ isActive }) => (
                  <CDBSidebarMenuItem icon="user" className={isActive ? "active" : ""}>
                    Moje konto
                  </CDBSidebarMenuItem>
                )}
              </NavLink>
              <NavLink to="/gym-pass">
                {({ isActive }) => (
                  <CDBSidebarMenuItem icon="id-card" className={isActive ? "active" : ""} iconClassName="sidebar-icon-small">
                    &nbsp;Karnety
                  </CDBSidebarMenuItem>
                )}
              </NavLink>
              <NavLink to="/diets">
                {({ isActive }) => (
                  <CDBSidebarMenuItem icon="utensils" className={isActive ? "active" : ""}>
                    Diety
                  </CDBSidebarMenuItem>
                )}
              </NavLink>
              <NavLink to="/training-plans">
                {({ isActive }) => (
                  <CDBSidebarMenuItem icon="clipboard-list" className={isActive ? "active" : ""}>
                    Plany treningowe
                  </CDBSidebarMenuItem>
                )}
              </NavLink>
              <NavLink to="/courses">
                {({ isActive }) => (
                  <CDBSidebarMenuItem icon="book" className={isActive ? "active" : ""}>
                    Kursy
                  </CDBSidebarMenuItem>
                )}
              </NavLink>
              <NavLink to="/Groupclass">
                {({ isActive }) => (
                  <CDBSidebarMenuItem icon="calendar-alt" className={isActive ? "active" : ""}>
                    Zajęcia grupowe
                  </CDBSidebarMenuItem>
                )}
              </NavLink>
              <NavLink to="/BmiCalculator">
                {({ isActive }) => (
                  <CDBSidebarMenuItem icon="calculator" className={isActive ? "active" : ""}>
                    Kalkulator BMI
                  </CDBSidebarMenuItem>
                )}
              </NavLink>
              <div>
                <div
                  className="sidebar-submenu-toggle"
                  onClick={() => setPersonalOpen(o => !o)}
                >
                  <CDBSidebarMenuItem icon="chart-line">
                    Treningi
                  </CDBSidebarMenuItem>
                </div>
                {personalOpen && (
                  <div className="sidebar-submenu">
                    <NavLink to="/personal-training/signup">
                      {({ isActive }) => (
                        <CDBSidebarMenuItem icon="calendar-plus" className={`submenu-item${isActive ? " active" : ""}`}>
                          Umów się
                        </CDBSidebarMenuItem>
                      )}
                    </NavLink>
                    <NavLink to="/personal-training/my-training">
                      {({ isActive }) => (
                        <CDBSidebarMenuItem icon="running" className={`submenu-item${isActive ? " active" : ""}`}>
                          Moje treningi
                        </CDBSidebarMenuItem>
                      )}
                    </NavLink>
                  </div>
                )}
              </div>
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
export default Sidebar;