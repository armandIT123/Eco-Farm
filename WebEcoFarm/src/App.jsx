import { Route, Routes } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css'
import Spinner from 'react-bootstrap/Spinner';

import Home from './Pages/Home'
import Discover from './Pages/Discover';
import Footer from './Components/Footer';
import Supplier from './Pages/Supplier';
import Cart from './Pages/Cart';
import NavBar from './Components/Navbar';
import Account from './Pages/Account';
import LoginModal from './LoginComponents/Login';
import PrivateRoutes from './Components/PrivateRoutes';

import { selectCurrentUser } from './Features/authSlice';
import { useSelector } from 'react-redux';
import { useCheckTokenQuery } from './Features/authApi';
import { useEffect, useState } from 'react';

export default function App() {

  const user = useSelector(selectCurrentUser);
  const [shouldFetch, setShouldFetch] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  const { data: userData, error } = useCheckTokenQuery(undefined, { skip: !shouldFetch });

  useEffect(() => {
    if (user === null) {
      setShouldFetch(true);
    } else {
      setIsLoading(false);
    }
  }, [user]);

  useEffect(() => {
    if (userData) {
      setIsLoading(false);
    }
    else if (error) {
      setIsLoading(false);
    }
  }, [userData, error]);

  if (isLoading) {
    return <Spinner className='spinner' />
  }

  return (
    <>
      <NavBar />
      <Routes>
        {/* Public routes*/}
        <Route path='/' element={<Home />} />
        <Route path='/descopera' element={<Discover />} />
        <Route path="/descopera/:name/:id" element={<Supplier />} />

        <Route element={<PrivateRoutes />}>
          <Route path='/cos' element={<Cart />} />
          <Route path='/profil/detalii' element={<Account />} />
          <Route path='/profil/adrese' element={<Account />} />
          <Route path='/profil/comenzi' element={<Account />} />
        </Route>
      </Routes>
      <Footer />

      <LoginModal />
    </>
  )
}
