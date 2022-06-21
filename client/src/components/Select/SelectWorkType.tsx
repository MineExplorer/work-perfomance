// TODO: import from DB
export enum WorkType {
    Development = 1,
    Testing = 2,
    Investigation = 3,
    Documentation = 4,
    Communication = 5,
    Meeting = 6
};

export function SelectWorkType(props: {value?: number, onChange?: any}) {
    return <select name="workType" value={props.value} onChange={props.onChange}>
        <option value={WorkType.Development}>Разработка</option>
        <option value={WorkType.Testing}>Тестирование</option>
        <option value={WorkType.Investigation}>Изучение</option>
        <option value={WorkType.Documentation}>Документация</option>
        <option value={WorkType.Communication}>Коммуникация</option>
        <option value={WorkType.Meeting}>Совещание</option>
    </select>
}