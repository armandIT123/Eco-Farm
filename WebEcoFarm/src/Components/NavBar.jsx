import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import "bootstrap-icons/font/bootstrap-icons.css";
import Button from "./Button";


function Navbar() {
    const [click, setClick] = useState(false);
    const [button, setButton] = useState(true);

    const handleClick = () => {
        setClick(!click);
        if (click) {
            document.body.style.overflow = 'hidden';
        } else {
            document.body.style.overflow = 'unset';
        }
    }
    const closeMobileMenu = () => setClick(false);

    const showButton = () => {
        if (window.innerWidth <= 768) {
            setButton(false);
        } else {
            setButton(true);
        }
    };

    useEffect(() => {
        showButton();
    }, []);

    window.addEventListener('resize', showButton);

    return (
        <>
            <nav className="navbar">
                <div className="navbar-container">
                    <Link to="/" className="navbar-logo" onClick={closeMobileMenu}>
                        Eco Farm
                    </Link>
                    <div className={click ? "menu-icon active" : "menu-icon"} onClick={handleClick}>
                        <i className={click ? "bi bi-x-square-fill" : "bi bi-card-text"}></i>
                    </div>
                    <ul className={click ? "nav-menu active" : "nav-menu"}>
                        <li className="nav-item">
                            <Link to='/' className="nav-links" onClick={closeMobileMenu}>
                                Acasă
                            </Link>
                        </li>
                        <li className="nav-item">
                            <Link to='/descopera' className="nav-links" onClick={closeMobileMenu}>
                                Descoperă
                            </Link>
                        </li>
                    </ul>
                    {button && <Button buttonStyle='btn--outline'>Sign up</Button>}
                </div>
            </nav>
        </>
    );
}

export default Navbar;