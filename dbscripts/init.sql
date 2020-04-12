CREATE TABLE groups
(
    id INTEGER PRIMARY KEY,
    name  VARCHAR (30)  NOT NULL,
    description  VARCHAR (100)  NOT NULL
);

CREATE TABLE users
(
    id INTEGER PRIMARY KEY,
    name  VARCHAR (30)  NOT NULL
);

CREATE TABLE user_groups
(
    group_id INTEGER REFERENCES groups(id) NOT NULL,
    user_id INTEGER REFERENCES users(id) NOT NULL,
    is_admin boolean NOT NULL,
    PRIMARY KEY (user_id, group_id)
);

INSERT INTO users(id, name) VALUES(1, 'John Doe');
INSERT INTO users(id, name) VALUES(2, 'Jane Doe');
INSERT INTO users(id, name) VALUES(3, 'Professor Moriarty');
INSERT INTO users(id, name) VALUES(4, 'Sherlock Homes');
INSERT INTO users(id, name) VALUES(5, 'Tony Stark');
INSERT INTO users(id, name) VALUES(6, 'Peter Parker');
INSERT INTO users(id, name) VALUES(7, 'Optimus Prime');
INSERT INTO users(id, name) VALUES(8, 'Dr. Watson');
INSERT INTO users(id, name) VALUES(9, 'Bill Gates');
INSERT INTO users(id, name) VALUES(10, 'Steven Jobs');
INSERT INTO users(id, name) VALUES(11, 'Elon Musk');

INSERT INTO groups(id, name, description) VALUES(101, 'Super Heros', 'A place for super heros to get together and discuss on how to save the world');
INSERT INTO groups(id, name, description) VALUES(201, 'Doe Family', 'A group for the Doe family to catch up at the end of the day');
INSERT INTO groups(id, name, description) VALUES(301, 'Detectives', 'Come join if you are into the art of detection and solving problems');
INSERT INTO groups(id, name, description) VALUES(401, 'Billion Dollar Club', 'Only for the high and mighty!');

INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(101, 5, false);
INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(101, 6, true);
INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(101, 7, false);

INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(201, 1, true);
INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(201, 2, false);

INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(301, 3, false);
INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(301, 4, true);
INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(301, 8, false);

INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(401, 9, true);
INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(401, 10, false);
INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(401, 11, false);
INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(401, 5, false);
