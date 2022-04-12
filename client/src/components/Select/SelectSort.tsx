type SortMode = 'AZ' | 'ZA' | 'New' | 'Old';

export function SelectSort(props: {value: SortMode, onChange: any}) {
    return <select value={props.value} onChange={props.onChange}>
        <option value={'AZ'}>А-Я</option>
        <option value={'ZA'}>Я-А</option>
        <option value={'New'}>Новые</option>
        <option value={'Old'}>Старые</option>
    </select>
}