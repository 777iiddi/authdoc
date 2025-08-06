import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import { Route ,BrowserRouter as Router, Routes, Navigate} from 'react-router-dom'
import Login from './Pages/Login'
import Register from './Pages/Register'
import Dashboard from './Pages/admin/Dashboard'
import Appointments from './Pages/Doctor/Appointments'
import Offers from './Pages/Patient/Offers'

function App() {
  return (
    <Router>
      <Routes>
          <Route path="/" element={<Navigate to="/login" />} />
          <Route path="/login" element={<Login />} />
          <Route path="/Register" element={<Register />} />
          <Route path="/Dashboard" element={<Dashboard />} />
          <Route path="/doctor/appointments" element={<Appointments />} />
          <Route path="/patient/offers" element={<Offers />} />
      </Routes>
      
    </Router>
     
        
    
  )
}

export default App
