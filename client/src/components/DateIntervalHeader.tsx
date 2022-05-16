import { useContext } from 'react';
import { Button } from '@mui/material';
import { ArrowBack, ArrowForward } from '@mui/icons-material';
import { SelectDateRange } from './Select';
import { observer } from 'mobx-react-lite';
import { DateIntervalContext } from '../stores/DateIntervalStore';

function DateIntervalHeader() {
	const store = useContext(DateIntervalContext);
	const {startDate, endDate, onDateRangeChange, changePeriod} = store;

	const intervalStr = startDate === endDate ? startDate.toLocaleDateString() : `${startDate.toLocaleDateString()} - ${endDate.toLocaleDateString()}`;
	return (
		<div style={{position: "relative", display: "flex", padding: 10}}>
			<SelectDateRange onChange={onDateRangeChange}/>
			<Button
				variant="contained"
				onClick={() => changePeriod(-1)}
				startIcon={<ArrowBack/>}
			/>
			<p style={{fontWeight: "bold", margin: 5}}>{intervalStr}</p>
			<Button
				variant="contained"
				onClick={() => changePeriod(1)}
				startIcon={<ArrowForward/>}
			/>
		</div>
	);
}

export default observer(DateIntervalHeader);