import React, { useState } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link, useNavigate } from 'react-router-dom';
import './NavMenu.css';
import { usePlayers } from '../context/PlayerContext';
export function NavMenu() {    
    const navigate = useNavigate();
    const { user, logout } = usePlayers();
    const [collapsed, setCollapsed] = useState(false);    

    const toggleNavbar = ()=> {
        setCollapsed(!collapsed);
    }
    const handleLogoutClick = (e) => {
        e.preventDefault(); 
        if (logout) {
            logout();
        }
        navigate('/login');
    };
    return (
        <header>
            <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" container light>
                <NavbarBrand tag={Link} to="/">APA Scores</NavbarBrand>
                <NavbarToggler onClick={toggleNavbar} className="mr-2" />
                <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!collapsed} navbar>
                    <ul className="navbar-nav flex-grow">
                        <NavItem>
                            <NavLink tag={Link} className="text-dark" to="/">Scores</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink tag={Link} className="text-dark" to="/stats">Stats</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink tag={Link} className="text-dark" to="/players">Add Players</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink tag={Link} className="text-dark" to="/settings"><i className="bi bi-gear-fill"></i></NavLink>
                        </NavItem>
                        {user && (
                            <NavItem>
                                <NavLink tag={Link} className="text-dark dropdown-item" to="#" onClick={handleLogoutClick}>Logout</NavLink>
                            </NavItem>
                        )}
                    </ul>
                </Collapse>
            </Navbar>
        </header >
    );
}
