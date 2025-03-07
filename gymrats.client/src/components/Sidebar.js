import React from 'react';
import {
  CDBSidebar,
  CDBSidebarContent,
  CDBSidebarHeader,
  CDBSidebarMenu,
  CDBSidebarMenuItem,
} from 'cdbreact';
import { NavLink } from 'react-router-dom';

const Sidebar = () => {
  return (
    <div style={{ display: 'flex', height: 'auto', overflow: 'scroll initial' }}>
      <CDBSidebar style={{
        backgroundColor: "#111317",
        color: "white",
        width:'120px',

      }}>
        <CDBSidebarHeader prefix={<i className="fa fa-bars fa-large"></i>} style={{ color: 'inherit' }} >
          <a href="/" className="text-decoration-none" style={{ color: 'inherit' }} >
            Imie Nazwisko
          </a>
        </CDBSidebarHeader>

        <CDBSidebarContent className="sidebar-content">
          <CDBSidebarMenu >
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
            <NavLink exact to="/" activeClassName="activeClicked">
              <CDBSidebarMenuItem icon="fa-solid fa-door-open">Wyloguj się</CDBSidebarMenuItem>
            </NavLink>
          </CDBSidebarMenu>
        </CDBSidebarContent>

      </CDBSidebar>
    </div>
  );
};

export default Sidebar;