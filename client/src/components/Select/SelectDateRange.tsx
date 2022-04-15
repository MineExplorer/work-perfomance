export function SelectDateRange(props: {onChange: any}) {
    return <select style={{marginLeft: '30px'}} onChange={props.onChange}>
        <option value={7}>Последние 7 дней</option>
        <option value={14}>Последниe 14 дней</option>
        <option value={30}>Последние 30 дней</option>
    </select>
}