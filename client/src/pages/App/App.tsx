import React from 'react';
import { BrowserRouter, Routes, Route, Navigate} from 'react-router-dom';

import './App.css';
import LoginPage from '../Login';
import DashboardPage from '../Dashboard';
import TeamDashboardPage from '../TeamDashboard';
import EmployeesPage from '../Employees';
import TimeReportPage from '../TimeReport';

export default function App() {
  return (
    <BrowserRouter>
      <div className="App">
        <div className="App-body">
          <div className="title">
            <h1>Work Perfomance</h1>
          </div>
          <Routes>
            <Route path="/" element={<Navigate replace to="/login" />} />
            <Route path="/timetable" element = {<TimeReportPage/>}/>
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