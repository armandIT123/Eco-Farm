import { useSelector } from 'react-redux';
import { Outlet, Navigate, useLocation } from 'react-router-dom'
import { selectCurrentUser } from '../Features/authSlice';
import { useEffect, useState } from 'react';

const PrivateRoutes = () => {

    const [prevPath, setPrevPath] = useState('');

    const user = useSelector(selectCurrentUser);
    const location = useLocation();

    useEffect(() => {
        // Update the previous path whenever the location changes
        setPrevPath(location.pathname);
        console.log('location', location.state);
    }, [location]);

    useEffect(() => {
        if (prevPath === '/supplier') {
            // Perform specific actions if coming from /supplier
            console.log('Came from /supplier');
        }
    }, [prevPath]);


    if (user !== null)
        return <Outlet />
    else {

        return <Navigate to="/" />
    }

}

export default PrivateRoutes