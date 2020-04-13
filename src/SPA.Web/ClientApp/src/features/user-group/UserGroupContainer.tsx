import React, { useEffect } from 'react';
import { Group } from "./Group";
import { Grid, Placeholder, Segment, CardGroup } from "semantic-ui-react";
import { useSelector, useDispatch } from 'react-redux';
import { loadAsync, selectLoadingStatus, selectGroups, selectCurrentUser, selectError } from "./userGroupSlice";
import { Redirect } from 'react-router-dom';

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

export const UserGroupContainer = () => {
    const dispatch = useDispatch();
    const loading = useSelector(selectLoadingStatus);
    const groups = useSelector(selectGroups);
    const currentUser = useSelector(selectCurrentUser);
    const error = useSelector(selectError);

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
                        : <CardGroup centered>
                            {
                                groups.map(g => <Group
                                    key={g.id}
                                    id={g.id}
                                    name={g.name}
                                    description={g.description}
                                    hasJoined={g.users.some(u => u.id === currentUser.id)}
                                    isAdmin={g.isAdmin}
                                    users={g.users.filter(u => u.id !== currentUser.id)} />)
                            }
                        </CardGroup>
            }
        </React.Fragment>
    )
}