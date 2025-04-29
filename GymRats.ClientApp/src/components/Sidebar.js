import React from 'react';
import {
  CDBSidebar,
  CDBSidebarContent,
  CDBSidebarHeader,
  CDBSidebarMenu,
  CDBSidebarMenuItem,
  CDBSidebarFooter
} from 'cdbreact';
import { NavLink } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import { useState, useEffect } from 'react';
import axios from 'axios';
function Sidebar() {
  const [personalData, setPersonalData] = useState(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const getUserPersonalData = async () => {
      const token = localStorage.getItem('token');
      if (!token) {
        setError('Brak tokena');
        setIsLoading(false);
        return;
      }

      try {
        const decodedPayload = JSON.parse(atob(token.split('.')[1]));
        const response = await axios.get(
          `https://localhost:44380/personal-data/${decodedPayload.email}`,
          { withCredentials: true }
        );
        setPersonalData(response.data);
      } catch (err) {
        setError(err.message);
      } finally {
        setIsLoading(false);
      }
    };

    getUserPersonalData();
  }, []);
  return (
    <div style={{ display: 'flex', height: '100%', overflow: 'scroll initial' }}>
      <CDBSidebar style={{
        backgroundColor: "#111317",
        color: "white",
        width: '120px',
        height: '100%'
      }} >
        <CDBSidebarHeader prefix={<i className="fa fa-bars fa-large"></i>} style={{ color: 'inherit' }} >
          {/* <Link to="/dashboard" className="text-decoration-none" style={{ color: 'inherit'}} >
            Imie Nazwisko
          </Link> */}
          {personalData && (
                  <div>
                    <p className="mb-1">{personalData.name} {personalData.surname}</p>
                  </div>
                )}
        </CDBSidebarHeader>

        <CDBSidebarContent className="sidebar-content" >
          <CDBSidebarMenu >
            <NavLink exact to="/user-profile" activeClassName="activeClicked">
              <CDBSidebarMenuItem icon="user">Moje konto</CDBSidebarMenuItem>
            </NavLink>
            <NavLink exact to="/gym-pass" activeClassName="activeClicked">
              <CDBSidebarMenuItem icon="ticket-alt">Karnety</CDBSidebarMenuItem>
            </NavLink>
            <NavLink exact to="/diets" activeClassName="activeClicked">
              <CDBSidebarMenuItem icon="carrot">Diety</CDBSidebarMenuItem>
            </NavLink>
            <NavLink exact to="/training-plans" activeClassName="activeClicked">
              <CDBSidebarMenuItem icon="clipboard-list">Plany treningowe</CDBSidebarMenuItem>
            </NavLink>
            <NavLink exact to="/courses" activeClassName="activeClicked">
              <CDBSidebarMenuItem icon="graduation-cap">Kursy</CDBSidebarMenuItem>
            </NavLink>
          </CDBSidebarMenu>
        </CDBSidebarContent>
        <CDBSidebarFooter style={{ textAlign: 'center' }}>
          <NavLink exact to="/" activeClassName="activeClicked" reloadDocument>
            <CDBSidebarMenuItem icon="fa-solid fa-door-open" style={{ color: "white" }}>Wyloguj się</CDBSidebarMenuItem>
          </NavLink>
        </CDBSidebarFooter>
      </CDBSidebar>
    </div>
  );
}

export default Sidebar;