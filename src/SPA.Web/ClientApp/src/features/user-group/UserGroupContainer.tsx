import React, { useEffect, useState } from 'react';
import { Group } from "./Group";
import { Grid, Placeholder, Segment, CardGroup, Button, GridColumn, GridRow, Input, Search } from "semantic-ui-react";
import { useSelector, useDispatch } from 'react-redux';
import { loadAsync, selectLoadingStatus, selectGroups, selectCurrentUser, selectError } from "./userGroupSlice";
import { Redirect } from 'react-router-dom';
import { GroupDetailsModal } from "./GroupDetailsModal";

export const UserGroupContainer = () => {
    const dispatch = useDispatch();
    const loading = useSelector(selectLoadingStatus);
    const groups = useSelector(selectGroups);
    const currentUser = useSelector(selectCurrentUser);
    const error = useSelector(selectError);
    const [searchTerm, setSearchTerm] = useState<string>('');

    useEffect(() => {
        dispatch(loadAsync());
    }, []);

    return (
        <React.Fragment>
            {
                error
                    ? <Redirect to="/login" />
                    :
                    loading === 'pending'
                        ? <PlaceholderExampleGrid />
                        :
                        <Grid columns={1}>
                            <GridColumn>
                                <GridRow style={{ marginBottom: '30px' }}>
                                    <Input fluid onChange={e => setSearchTerm(e.target.value)} action={{ icon: 'search' }} placeholder='Search for a group...' />
                                </GridRow>
                                <GridRow>
                                    <CardGroup centered>
                                        {
                                            [...groups]
                                            .filter(g => g.name.toLocaleLowerCase().startsWith(searchTerm.toLocaleLowerCase()))
                                            .map(g => <Group
                                                    key={g.id}
                                                    id={g.id}
                                                    name={g.name}
                                                    description={g.description}
                                                    hasJoined={g.users.some(u => u.id === currentUser.id)}
                                                    isAdmin={g.isAdmin}
                                                    users={g.users.filter(u => u.id !== currentUser.id)} />
                                                )
                                        }
                                    </CardGroup>
                                </GridRow>
                                <GridRow style={{ marginTop: '30px' }}>
                                    <GroupDetailsModal action='Add' desc={""} id={0} name={""} />
                                </GridRow>
                            </GridColumn>
                        </Grid>
            }
        </React.Fragment>
    )
}

const PlaceholderExampleGrid = () => (
    <Grid columns={3} stackable>
        <Grid.Column>
            <Segment raised>
                <Placeholder>
                    <Placeholder.Header>
                        <Placeholder.Line />
                        <Placeholder.Line />
                    </Placeholder.Header>
                    <Placeholder.Paragraph>
                        <Placeholder.Line length='medium' />
                        <Placeholder.Line length='short' />
                    </Placeholder.Paragraph>
                </Placeholder>
            </Segment>
        </Grid.Column>

        <Grid.Column>
            <Segment raised>
                <Placeholder>
                    <Placeholder.Header>
                        <Placeholder.Line />
                        <Placeholder.Line />
                    </Placeholder.Header>
                    <Placeholder.Paragraph>
                        <Placeholder.Line length='medium' />
                        <Placeholder.Line length='short' />
                    </Placeholder.Paragraph>
                </Placeholder>
            </Segment>
        </Grid.Column>

        <Grid.Column>
            <Segment raised>
                <Placeholder>
                    <Placeholder.Header>
                        <Placeholder.Line />
                        <Placeholder.Line />
                    </Placeholder.Header>
                    <Placeholder.Paragraph>
                        <Placeholder.Line length='medium' />
                        <Placeholder.Line length='short' />
                    </Placeholder.Paragraph>
                </Placeholder>
            </Segment>
        </Grid.Column>
    </Grid>
)