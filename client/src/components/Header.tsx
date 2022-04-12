import { NavLink } from "react-router-dom";

export default function AppHeader() {	
	return (
		<>
			<ul className="header" style={{
				backgroundColor: "#2452e6",
				padding: 0,
				userSelect: "none",
				display: "inline-block"
			}}>
				<li><NavLink to="/timetable">Тайм-репорты</NavLink></li>
				<li><NavLink to="/stats">Статистика</NavLink></li>
				<li><NavLink to="/employees">Сотрудники</NavLink></li>
				<li><NavLink to="/login">Выход</NavLink></li>
			</ul>
		</>
	);
}