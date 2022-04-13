import { Project } from "../../data"

export function SelectProject(props: {projects: Project[], onChange: any}) {

    const optionsList = props.projects.map(p => {
        return <option value={p.id}>{p.title}</option>
    });

    return <select style={{position: "absolute", right: 100}} onChange={props.onChange}>
        {optionsList}
    </select>
}