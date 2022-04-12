import { Seniority } from "../../data";

export function SelectSeniority(props: {value: number, onChange: any}) {
    return <select name="seniority" value={props.value} onChange={props.onChange}>
        <option value={Seniority.NotAvailable}>Не указано</option>
        <option value={Seniority.Junior}>Junior</option>
        <option value={Seniority.JuniorMiddle}>JuniorMiddle</option>
        <option value={Seniority.Middle}>Middle</option>
        <option value={Seniority.MiddleSenior}>MiddleSenior</option>
        <option value={Seniority.Senior}>Senior</option>
    </select>
}