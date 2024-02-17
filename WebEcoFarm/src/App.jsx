import { useState } from 'react'
import { Route, Routes } from 'react-router-dom';

import 'bootstrap/dist/css/bootstrap.min.css'

import NavBar from './Components/NavBar';
import Home from './Pages/Home'
import Discover from './Pages/Discover';
import Footer from './Components/Footer';
import Supplier from './Pages/Supplier';

export default function App() {
  return (
    <>
      <NavBar />
      <div className="container">
        <Routes>
          <Route path='/' element={<Home />} />
          <Route path='/descopera' element={<Discover />} />
          <Route path="/descopera/:name/:id" element={<Supplier />} />
        </Routes>
      </div>
      <Footer />
    </>
  )
}
