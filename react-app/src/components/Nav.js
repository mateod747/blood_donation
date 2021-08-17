import React from 'react';
import '../App.css';
import '../layouts/Nav.css'
import { Link } from 'react-router-dom';
import logo from '../public/logo-150.png';

function Nav() {

    const navStyle = {
        'font-family': 'monospace',
        'color': 'rgb(132, 163, 163)',
        'font-size': '20px',
        'text-decoration': 'none'
    };

    return (
        <nav>
            <div className="nav-links">
                <div className="nav-logo">
                    <img src={logo} />
                </div>
                <div className="link-div">
                    <div className="link"> 
                    <Link style={navStyle} to="/dashboard">
                        Dashboard
                    </Link>
                    </div>
                    <div className="link">
                    <Link style={navStyle} to="/dashboard">
                        Dashboard
                    </Link>
                    </div>
                </div>
            </div>
        </nav>
    );
}

export default Nav;