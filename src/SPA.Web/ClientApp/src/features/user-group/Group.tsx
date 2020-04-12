import React from 'react';
import { Card, Icon, Button, Dropdown, Grid, Placeholder, Segment, CardGroup } from "semantic-ui-react";

const description = [
    "Amy is a violinist with 2 years experience in the wedding industry.",
    "She enjoys the outdoors and currently resides in upstate New York."
].join(" ");

const prevent = (e: Event) => {
    e.preventDefault();
    e.stopPropagation();
}

export const Group = () => (
    <Card>
        <Card.Content header="Group Name" />
        <Card.Content description={description} />
        <Card.Content extra>
            <Button icon>
                <Icon name="plus" /> Join
      </Button>
            <Button disabled icon>
                <Icon name="check" /> Joined
      </Button>

            <Dropdown
                text="Users"
                floating
                labeled
                button
                icon="remove user"
                className="icon"
            >
                <Dropdown.Menu onClick={prevent}>
                    <Dropdown.Item icon="user" disabled text="You" />
                    <Dropdown.Item text="John Doe" icon="remove" />
                </Dropdown.Menu>
            </Dropdown>
        </Card.Content>
    </Card>
)
