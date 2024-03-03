import { Route, Routes } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css'

import Home from './Pages/Home'
import Discover from './Pages/Discover';
import Footer from './Components/Footer';
import Supplier from './Pages/Supplier';
import Cart from './Pages/Cart';
import NavBar from './Components/Navbar';

export default function App() {
  return (
    <>
      <NavBar />
      <Routes>
        {/* Public routes*/}
        <Route path='/' element={<Home />} />
        <Route path='/descopera' element={<Discover />} />
        <Route path="/descopera/:name/:id" element={<Supplier />} />

        {/*Private routes*/}
        <Route path='/cos' element={<Cart />} />

      </Routes>
      <Footer />
    </>
  )
}
