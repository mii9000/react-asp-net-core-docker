import React from 'react';
import { Card, Icon, Button, Dropdown } from "semantic-ui-react";
import { User, joinAsync, removeAsync } from "./userGroupSlice";
import { useDispatch } from 'react-redux';
import { GroupDetailsModal } from "./GroupDetailsModal";

interface GroupProps {
    id: number
    name: string
    description: string
    hasJoined: boolean
    isAdmin: boolean
    users: User[]
}

const prevent = (e: Event) => {
    e.preventDefault();
    e.stopPropagation();
}

export const Group = (props: GroupProps) => {
    const dispatch = useDispatch();
    return (
        <Card>
            <Card.Content header={props.name}>
            </Card.Content>
            <Card.Content description={props.description} />
            <Card.Content extra>
                {
                    props.hasJoined
                        ? !props.isAdmin ? <Button disabled icon> <Icon name="check" /> Joined</Button> : null
                        : <Button icon onClick={_ => dispatch(joinAsync(props.id, props.name))}>
                            <Icon name="plus" /> Join
                        </Button>
                }
                {
                    props.isAdmin
                        ?
                        <React.Fragment>
                            <Dropdown
                                text="Users"
                                floating
                                labeled
                                button
                                icon="remove user"
                                className="icon">
                                <Dropdown.Menu onClick={prevent}>
                                    <Dropdown.Item icon="user" disabled text="You" />
                                    {
                                        props.users.map(u =>
                                            <Dropdown.Item key={u.id} text={u.name}
                                                icon="remove" onClick={_ => dispatch(removeAsync(u.id, props.id, u.name, props.name))} />)
                                    }
                                </Dropdown.Menu>
                            </Dropdown>
                            <GroupDetailsModal action='Update' desc={props.description} id={props.id} name={props.name} />
                        </React.Fragment>
                        : null
                }
            </Card.Content>
        </Card>
    )
}
