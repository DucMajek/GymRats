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
    backgroundColor: "var(--sidebar_color)",
    color: "var(--sidebar_text_color)",
    transition: "background-color 3s",
  }}>
        <CDBSidebarHeader prefix={<i className="fa fa-bars fa-large"></i>} style={{ color: 'inherit' }} >
          <a href="/" className="text-decoration-none" style={{ color: 'inherit' }} >
            Imie Nazwisko
          </a>
        </CDBSidebarHeader>

        <CDBSidebarContent className="sidebar-content" >
          <CDBSidebarMenu >
            <NavLink exact to="/gym-pass" activeClassName="activeClicked">
              <CDBSidebarMenuItem icon = "ticket-alt" style={{ color: 'var(--body_color)' }}>Karnety</CDBSidebarMenuItem>
            </NavLink>
            <NavLink exact to="/diets" activeClassName="activeClicked">
              <CDBSidebarMenuItem icon = "carrot" style={{ color: 'var(--body_color)' }}>Diety</CDBSidebarMenuItem>
            </NavLink>
            <NavLink exact to="/training-plans" activeClassName="activeClicked">
              <CDBSidebarMenuItem icon = "clipboard-list" style={{ color: 'var(--body_color)' }}>Plany treningowe</CDBSidebarMenuItem>
            </NavLink>
            <NavLink exact to="/courses" activeClassName="activeClicked">
              <CDBSidebarMenuItem icon = "graduation-cap" style={{ color: 'var(--body_color)' }}>Kursy</CDBSidebarMenuItem>
            </NavLink>
          </CDBSidebarMenu>
        </CDBSidebarContent>

      </CDBSidebar>
    </div>
  );
};

export default Sidebar;