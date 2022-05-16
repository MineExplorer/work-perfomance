import { Project } from "../../data"

export function SelectProject(props: {projects: Project[], onChange?: any, value?: number}) {

    const optionsList = props.projects.map(p => {
        return <option value={p.id}>{p.title}</option>
    });

    return (
        <select
            style={{padding: '5px 0px'}}
            value={props.value}
            onChange={props.onChange}
        >
            {optionsList}
        </select>
    )
}