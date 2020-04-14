CREATE TABLE groups
(
    id SERIAL PRIMARY KEY,
    name  VARCHAR (30)  NOT NULL,
    description  VARCHAR (100)  NOT NULL
);

CREATE TABLE users
(
    id SERIAL PRIMARY KEY,
    name  VARCHAR (30)  NOT NULL,
    email VARCHAR (30)  NOT NULL
);

CREATE TABLE user_groups
(
    group_id SERIAL REFERENCES groups(id) NOT NULL,
    user_id SERIAL REFERENCES users(id) NOT NULL,
    is_admin boolean NOT NULL,
    PRIMARY KEY (user_id, group_id)
);

INSERT INTO users(id, name, email) VALUES(1, 'John Doe', 'example@org.com');
INSERT INTO users(id, name, email) VALUES(2, 'Jane Doe', 'example@org.com');
INSERT INTO users(id, name, email) VALUES(3, 'Professor Moriarty', 'example@org.com');
INSERT INTO users(id, name, email) VALUES(4, 'Sherlock Homes', 'example@org.com');
INSERT INTO users(id, name, email) VALUES(5, 'Tony Stark', 'example@org.com');
INSERT INTO users(id, name, email) VALUES(6, 'Peter Parker', 'example@org.com');
INSERT INTO users(id, name, email) VALUES(7, 'Optimus Prime', 'example@org.com');
INSERT INTO users(id, name, email) VALUES(8, 'Dr. Watson', 'example@org.com');
INSERT INTO users(id, name, email) VALUES(9, 'Bill Gates', 'example@org.com');
INSERT INTO users(id, name, email) VALUES(10, 'Steven Jobs', 'example@org.com');
INSERT INTO users(id, name, email) VALUES(11, 'Elon Musk', 'example@org.com');
INSERT INTO users(id, name, email) VALUES(12, 'Mohammed Ibrahim Islam', 'dev.ibrahim.islam@gmail.com');

INSERT INTO groups(id, name, description) VALUES(1, 'Super Heros', 'A place for super heros to get together and discuss on how to save the world');
INSERT INTO groups(id, name, description) VALUES(2, 'Doe Family', 'A group for the Doe family to catch up at the end of the day');
INSERT INTO groups(id, name, description) VALUES(3, 'Detectives', 'Come join if you are into the art of detection and solving problems');
INSERT INTO groups(id, name, description) VALUES(4, 'Billion Dollar Club', 'Only for the high and mighty!');

INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(1, 5, false);
INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(1, 6, false);
INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(1, 7, false);
INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(1, 12, true);

INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(2, 1, true);
INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(2, 2, false);

INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(3, 3, false);
INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(3, 4, false);
INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(3, 8, false);
INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(3, 12, true);

INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(4, 9, true);
INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(4, 10, false);
INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(4, 11, false);
INSERT INTO user_groups(group_id, user_id, is_admin) VALUES(4, 5, false);

SELECT SETVAL('users_id_seq', (SELECT MAX(id) FROM users));

SELECT SETVAL('groups_id_seq', (SELECT MAX(id) FROM groups));

SELECT currval('users_id_seq');

SELECT currval('groups_id_seq');
