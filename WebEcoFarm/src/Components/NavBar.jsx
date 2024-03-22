import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import Offcanvas from 'react-bootstrap/Offcanvas';

import { useEffect, useState } from 'react';

import { useDispatch, useSelector } from 'react-redux';
import { selectCurrentUser } from '../Features/authSlice';

import avatar from "../../public/avatar_male.png"
import { openModal } from '../Features/loginModalSlice';

export default function NavBar() {

    const [userLogged, setUserLogged] = useState(false);

    const user = useSelector(selectCurrentUser);
    const dispatch = useDispatch();

    const handleShowModal = () => {
        dispatch(openModal());
    }

    useEffect(() => {
        setUserLogged(user !== null);
    }, [user]);

    return (
        <>
            <Navbar key='md' expand='md' className="bg-body-tertiary mb-3 navbar">
                <Container fluid className='navbar-container'>
                    <Navbar.Brand href="/">Eco Farm</Navbar.Brand>
                    <Navbar.Toggle aria-controls={`offcanvasNavbar-expand-md`} />
                    <Navbar.Offcanvas
                        id={`offcanvasNavbar-expand-md`}
                        aria-labelledby={`offcanvasNavbarLabel-expand-md`}
                        placement="end"
                    >
                        <Offcanvas.Header closeButton>
                            <Offcanvas.Title id={`offcanvasNavbarLabel-expand-md`}>
                                Eco Farm
                            </Offcanvas.Title>
                        </Offcanvas.Header>
                        <Offcanvas.Body>
                            <Nav className="justify-content-end flex-grow-1 pe-3">
                                <Nav.Link href="/" className='nav-links'>Acasă</Nav.Link>
                                <Nav.Link href="/descopera" className='nav-links'>Descoperă</Nav.Link>
                                {
                                    userLogged ?
                                        <Nav.Link href="/profil/detalii" ><img src={avatar} height={32} /></Nav.Link> :
                                        <i onClick={handleShowModal} className="bi bi-person-circle nav-links" />
                                }
                            </Nav>
                        </Offcanvas.Body>
                    </Navbar.Offcanvas>
                </Container>
            </Navbar>
        </>
    )
}
