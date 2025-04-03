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
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
function Sidebar() {

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
          Imie Nazwisko
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