export function SelectTime(props: {value?: number, onChange?: any}) {
    return <select name="duration" value={props.value} onChange={props.onChange}>
        <option>0.25</option>
        <option>0.5</option>
        <option>0.75</option>
        <option>1</option>
        <option>1.25</option>
        <option>1.5</option>
        <option>1.75</option>
        <option>2</option>
        <option>2.25</option>
        <option>2.5</option>
        <option>2.75</option>
        <option>3</option>
    </select>
}