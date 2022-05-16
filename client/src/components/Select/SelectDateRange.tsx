export enum DateRange {
    Day,
    Week,
    Month
}

export function SelectDateRange(props: {onChange: any}) {
    return <select defaultValue={DateRange.Week} style={{margin: '0px 10px'}} onChange={props.onChange}>
        <option value={DateRange.Day}>День</option>
        <option value={DateRange.Week}>Неделя</option>
        <option value={DateRange.Month}>Месяц</option>
    </select>
}