import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom';
import Profile from '../AccountComponents/Profile';
import { useSelector } from 'react-redux';
import { selectCurrentUser } from '../Features/authSlice';
import Orders from '../AccountComponents/Orders';

import userImg from "../../public/user.png"
import userSelectedImg from "../../public/user-selected.png"
import pinImg from "../../public/pin.png"
import pinSelectedImg from "../../public/pin-selected.png"
import orderImg from "../../public/order.png"
import orderSelectedImg from "../../public/order-selected.png"

function Account() {

    const user = useSelector(selectCurrentUser);
    const Href = window.location.href;
    let navigate = useNavigate();

    const ElementClicked = (element) => {
        navigate('/profil/' + element, { replace: true });
    }

    return (
        <div className='account-container'>
            <div className='account-sidebar'>
                <div className={`sidebar-element ${Href.endsWith('/detalii') ? ' selected' : ''}`} onClick={() => ElementClicked('detalii')}>
                    <img src={Href.endsWith('/detalii') ? userSelectedImg : userImg} height={24} />
                    <p>Profil</p>
                </div>
                <div className={`sidebar-element ${Href.endsWith('/adrese') ? ' selected' : ''}`} onClick={() => ElementClicked('adrese')}>
                    <img src={Href.endsWith('/adrese') ? pinSelectedImg : pinImg} height={24} />
                    <p>Adresele mele</p>
                </div>
                <div className={`sidebar-element ${Href.endsWith('/comenzi') ? ' selected' : ''}`} onClick={() => ElementClicked('comenzi')}>
                    <img src={Href.endsWith('/comenzi') ? orderSelectedImg : orderImg} height={24} />
                    <p>Comenzile mele</p>
                </div>
            </div>

            <div className='account-content'>
                {Href.endsWith('/detalii') && <Profile name={user?.userName} email={user?.email} phoneNo={user?.phoneNo} />}
                {Href.endsWith('/comenzi') && <Orders />}




            </div>
        </div>
    )
}

export default Account