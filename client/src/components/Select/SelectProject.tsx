import { Project } from "../../data"

export function SelectProject(props: {projects: Project[], onChange?: any, value?: number, style?: any}) {

    const optionsList = props.projects.filter(p => !p.archived).map(p => {
        return <option value={p.id}>{p.title}</option>
    });

    return (
        <select
            name="project"
            style={props.style}
            value={props.value}
            onChange={props.onChange}
        >
            {optionsList}
        </select>
    )
}