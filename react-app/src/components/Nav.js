import React from 'react';
import '../App.scss';
import '../layouts/Nav.css'

function Nav() {
    return (
        <nav>
            <div className="nav-links">
                <div className="nav-logo">
                    Sustav  <br></br>
                    za <br></br>
                    praćenje  <br></br>
                    doniranja <br></br>
                    krvi
                </div>
                <div className="link-div">
                    <a href="http://localhost:3000/dashboard">
                        <div className="link">
                            Osnovni podaci
                        </div>
                    </a>
                    <a href="http://localhost:3000/donation-list">
                        <div className="link">
                            Povijest doniranja
                        </div>
                    </a>
                </div>
            </div>
        </nav>
    );
}

export default Nav;