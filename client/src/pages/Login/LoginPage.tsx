import React from 'react';
import {NavLink} from 'react-router-dom';
import './LoginPage.css';

export default function LoginPage() {
	function authorize() {
	}

	return (
		<div className="LoginCard">
			<p>Email</p>
			<input id="login"/>
			<p>Пароль</p>
			<input id="password" type="password"/>
			<NavLink to="/timetable" className="buttonEnter" onClick={authorize}>Вход</NavLink>
		</div>
	)
}