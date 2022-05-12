export enum TimeRange {
    Day,
    Week,
    Month
}

export function SelectTimeRange(props: {onChange: any}) {
    return <select defaultValue={TimeRange.Week} style={{marginLeft: '30px'}} onChange={props.onChange}>
        <option value={TimeRange.Day}>День</option>
        <option value={TimeRange.Week}>Неделя</option>
        <option value={TimeRange.Month}>Месяц</option>
    </select>
}