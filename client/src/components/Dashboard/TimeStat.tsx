import { observer } from 'mobx-react';
import { useContext, useEffect, useRef, useState } from 'react';
import { fetchFunctionApi, lineColors } from '../../helpers';
import { DateIntervalContext } from '../../stores/DateIntervalStore';

const fillColors = [
  "#6495ed",
  "#2db92d",
  "#DC143C"
]

function TimeStat(props: {employeeId: number}) {
  const store = useContext(DateIntervalContext);
	const {startDate, endDate} = store;
  
  const [workTypes, setWorkTypes] = useState({});
  const [totalTime, setTotalTime] = useState({});

  const canvasRef = useRef(null);

  useEffect(() => {
		loadTimeData()
		.then(
			(result) => {
				setTotalTime(result);
			}
		)
		.catch(error => {
			console.log(error);
		});

		loadWorkTypes()
		.then(
			(result) => {
				setWorkTypes(result);
			}
		)
		.catch(error => {
			console.log(error);
		});
	}, [props, startDate, endDate]);

  useEffect(() => {
    const array = convertTimeToPercentage();
    let prevVal = 0;
    console.log("start draw: " + array);
    const canvasEl = canvasRef.current
    const ctx = canvasEl.getContext('2d');
    ctx.clearRect(0, 0, canvasEl.width, canvasEl.height);
    for (let i in array) {
      const val = array[i] * canvasEl.width;
      ctx.fillStyle = fillColors[parseInt(i) % fillColors.length];
      ctx.fillRect(prevVal, 0, val, 100);
      prevVal += val;
    }
  }, [totalTime]);

  const convertTimeToPercentage = (): number[] => {
    let sum = 0;
    for (let key in totalTime) {
      sum += totalTime[key];
    }
    const array = [];
    for (let key in totalTime) {
      array.push(totalTime[key] / sum);
    }
    return array;
  }

  async function loadTimeData() {
    const dateStart = store.getStartDateStr();
    const dateEnd = store.getEndDateStr();
    return await fetchFunctionApi<object>(`/TimeInterval/worktypes?employeeId=${props.employeeId}&dateStart=${dateStart}&dateEnd=${dateEnd}`);
	}

  async function loadWorkTypes() {
    //return await fetchFunctionApi<WorkType>('/WorkType');
    return {
      1: 'Разработка',
      2: 'Тестирование',
      3: 'Изучение',
      4: 'Документация',
      5: 'Коммуникация',
      6: 'Совещание'
    }
  }

  const timeText = [];
  let index = 0;

  for (let key in totalTime) {
    timeText.push(
      <p key={key} style={{color: fillColors[index++ % fillColors.length]}}>
        {`${workTypes[key]}: ${totalTime[key]} ч`}
      </p>
    );
  }

  return (
    <div style={{marginLeft: 50, textAlign: "left"}}>
      <h3>Продуктивность</h3>
      <canvas ref={canvasRef} width={500} height={50}/>
      {timeText}
      <br/><p>Решено задач: 11</p>
      <p>Время выполнения: 55 ч</p>
      <p>Трудоёмкость задач: 45 ч</p>
      <p>Продуктивность решения задач: 0.81</p>
      <p>Эффективность: 0.9</p>
    </div>
  );
}

export default observer(TimeStat);