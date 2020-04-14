import React, { useState } from 'react';
import { Icon, Button, Modal, Form, Header } from "semantic-ui-react";
import { createAsync, updateAsync } from "./userGroupSlice";
import { useDispatch } from 'react-redux';

type Action = 'Add' | 'Update'

interface GroupDetailsModalProps {
    action: Action
    id: number
    name: string
    desc: string 
}

export const GroupDetailsModal = (props: GroupDetailsModalProps) => {
    const dispatch = useDispatch();
    const [userName, setUserName] = useState<string>(props.name);
    const [description, setDescription] = useState<string>(props.desc);
    const [open, setOpen] = useState<boolean>(false);
    const save = () => {
        props.action === 'Add' 
        ? dispatch(createAsync(userName, description))
        : dispatch(updateAsync(props.id, userName, description));
        setOpen(false);
    }

    return (
        <Modal onClose={_ => setOpen(false)} open={open} trigger={<Button onClick={_ => setOpen(true)} icon><Icon name="edit" />Edit</Button>} size="tiny">
            <Header icon="edit" content="Group Details" />
            <Modal.Content>
                <Form>
                    <Form.Field>
                        <input placeholder="Name" value={userName}
                            onChange={e => setUserName(e.target.value)} />
                    </Form.Field>
                    <Form.Field>
                        <input placeholder="Description" value={description}
                            onChange={e => setDescription(e.target.value)} />
                    </Form.Field>
                </Form>
            </Modal.Content>
            <Modal.Actions>
                <Button color="green" onClick={save}>
                    <Icon name="checkmark" /> Save
                </Button>
            </Modal.Actions>
        </Modal>
    );
}