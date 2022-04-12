import React from "react";

export default function DiagramHeader(props: {dateStart: string, dateEnd: string}) {
	return (
		<div style={{position: "relative", display: "flex", padding: 10}}>
			<p style={{fontWeight: "bold"}}>{props.dateStart} - {props.dateEnd}</p>
			<select style={{position: "absolute", right: 0}}>
				<option>Последние 7 дней</option>
				<option>Последниe 14 дней</option>
				<option>Последние 30 дней</option>
			</select>
		</div>
	);
}