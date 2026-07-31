import React, { Component } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import { PlayerContext } from '../context/PlayerContext';
export class NavMenu extends Component {
    static displayName = NavMenu.name;
    static contextType = PlayerContext;
    constructor(props) {
        super(props);
        console.log(props);
        //const { user } = usePlayers();
        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true
        };
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }
    handleLogoutClick = (e) => {
        e.preventDefault(); // Prevents the browser from jumping or reloading via the href  
        if (this.context && this.context.logout) {
            this.context.logout();
        }
        window.location.href = '/login';
    };
    render() {
        const { user } = this.context || {};
        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" container light>
                    <NavbarBrand tag={Link} to="/">APA Scores</NavbarBrand>
                    <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                    <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
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
                                    <NavLink tag={Link} className="text-dark dropdown-item" to="#" onClick={this.handleLogoutClick}>Logout</NavLink>
                                </NavItem>
                            )}
                        </ul>
                    </Collapse>
                </Navbar>
            </header >
        );
    }
}
