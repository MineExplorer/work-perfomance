import React, { useState } from 'react';
import { BrowserRouter, Routes, Route, Navigate} from 'react-router-dom';
import './App.css';
import LoginPage from './pages/Login';
import DashboardPage from './pages/Dashboard';
import TeamDashboardPage from './pages/TeamDashboard';
import EmployeesPage from './pages/Employees';

function App() {
  const [auth, setAuth] = useState(false);

  return (
    <BrowserRouter>
      <div className="App">
        <div className="App-body">
          <div className="title">
            <h1>Work Perfomance</h1>
          </div>
          <Routes>
            <Route path="/" element={<Navigate replace to="/login" />} />
            <Route path="/timetable" element = {<DashboardPage/>}/>
            <Route path="/dashboard" element = {<DashboardPage/>}/>
            <Route path="/teamdashboard" element = {<TeamDashboardPage/>}/>
            <Route path="/employees" element = {<EmployeesPage/>}/>
            <Route path="/login" element = {<LoginPage/>}/>
          </Routes>
        </div>
      </div>
    </BrowserRouter>
  );
}

export default App;
