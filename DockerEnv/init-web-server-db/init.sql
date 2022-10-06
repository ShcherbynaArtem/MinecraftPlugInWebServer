CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE IF NOT EXISTS users (
	id uuid PRIMARY KEY NOT NULL,
	username VARCHAR (20) UNIQUE NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    ip VARCHAR (30) NOT NUll,
	lives int NOT NULL DEFAULT 0
);

CREATE TABLE IF NOT EXISTS departments
(
    id int PRIMARY KEY,
    name text NOT NULL,
    description text
);

CREATE TABLE IF NOT EXISTS product_types
(
    id int PRIMARY KEY,
    name text NOT NULL,
    department int references departments(id)
);

CREATE TABLE IF NOT EXISTS products
(
    id uuid PRIMARY KEY DEFAULT uuid_generate_v1 (),
    name text NOT NULL,
    description text NOT NULL,
    type int references product_types(id),
    price numeric NOT NULL DEFAULT 0,
	availability bool NOT NULL DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS user_items
(
	id uuid PRIMARY KEY DEFAULT uuid_generate_v1 (),
    user_id uuid references users(id) ON DELETE CASCADE,
    item_id uuid references products(id) NOT NULL
);

CREATE TABLE IF NOT EXISTS user_perks
(
	id uuid PRIMARY KEY DEFAULT uuid_generate_v1 (),
    user_id uuid references users(id) ON DELETE CASCADE,
    perk_id uuid references products(id) NOT NULL
);

CREATE TABLE IF NOT EXISTS bundles
(
    id uuid PRIMARY KEY DEFAULT uuid_generate_v1 (),
    name text NOT NULL,
    discount numeric NOT NULL DEFAULT 0 
);

CREATE TABLE IF NOT EXISTS bundle_products
(
    id uuid PRIMARY KEY DEFAULT uuid_generate_v1 (),
    bundle_id uuid references bundles(id) ON DELETE CASCADE,
    product_id uuid references products(id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS transactions
(
	id uuid PRIMARY KEY DEFAULT uuid_generate_v1 (),
	user_id uuid,
	user_email text,
	product_id uuid,
	product_name text,
	amount numeric	
);

INSERT INTO users (id, username, email, ip, lives) VALUES ('0bb54b71-b962-43e1-8209-e9ff8b79dbc1', 'test_user_bob', 'kabumuty@gmail.com', 'localhost', 3);

INSERT INTO departments (id, name, description) VALUES (1, 'access', 'access to the server');
INSERT INTO departments (id, name, description) VALUES (2, 'equipment', 'hat, armor, pants, shoes');
INSERT INTO departments (id, name, description) VALUES (3, 'weapon', 'sword, bow...');
INSERT INTO departments (id, name, description) VALUES (4, 'perk', 'some perks for user');

INSERT INTO product_types (id, name, department) VALUES (1, 'Life', 1);
INSERT INTO product_types (id, name, department) VALUES (2, 'Helmet', 2);
INSERT INTO product_types (id, name, department) VALUES (3, 'Armor', 2);
INSERT INTO product_types (id, name, department) VALUES (4, 'Pants', 2);
INSERT INTO product_types (id, name, department) VALUES (5, 'Shoes', 2);
INSERT INTO product_types (id, name, department) VALUES (6, 'Damage+', 4);

INSERT INTO products (id, name, description, type, price) VALUES ('c1ade2fd-0bab-4435-8b63-7af77fa22319', '1 life', 'give you access to the server', 1, 1);
INSERT INTO products (id, name, description, type, price) VALUES ('1d89781b-91b5-4df0-92eb-a4dac5833fa3', 'Paper hat', 'top 1 MLG helmet', 2, 5);
INSERT INTO products (id, name, description, type, price) VALUES ('deeb55e0-1463-4a42-998c-56fd43692788', 'Paper armor', 'top 1 MLG armor', 3, 5);
INSERT INTO products (id, name, description, type, price) VALUES ('5c69487f-6448-44a9-a8a7-5c1332501f51', 'Paper pants', 'top 1 MLG pants', 4, 5);
INSERT INTO products (id, name, description, type, price) VALUES ('8a825f34-94a9-4e74-9f22-6a901273d6bd', 'Paper shoes', 'top 1 MLG shoes', 5, 5);
INSERT INTO products (id, name, description, type, price) VALUES ('016437f9-2b33-4a6b-a4c1-7676e42d398c', 'God`s blessing', 'increase your damage to 1kk', 6, 100);

INSERT INTO bundles (id, name, discount) VALUES ('3370c11e-208f-4c78-ad7d-86a256b1c49a', 'Paper set', 20);

INSERT INTO bundle_products (id, bundle_id, product_id) VALUES ('f34b024a-451f-4d80-b6eb-11736bbdbfab', '3370c11e-208f-4c78-ad7d-86a256b1c49a', '1d89781b-91b5-4df0-92eb-a4dac5833fa3');
INSERT INTO bundle_products (id, bundle_id, product_id) VALUES ('b7840cbd-d3e1-4a02-b470-2cb26911b16a', '3370c11e-208f-4c78-ad7d-86a256b1c49a', 'deeb55e0-1463-4a42-998c-56fd43692788');
INSERT INTO bundle_products (id, bundle_id, product_id) VALUES ('da0a5be8-c646-40cc-b2b1-86f79c1c0cd0', '3370c11e-208f-4c78-ad7d-86a256b1c49a', '5c69487f-6448-44a9-a8a7-5c1332501f51');
INSERT INTO bundle_products (id, bundle_id, product_id) VALUES ('beaf7cf2-1f91-407d-9491-716f4c291a42', '3370c11e-208f-4c78-ad7d-86a256b1c49a', '8a825f34-94a9-4e74-9f22-6a901273d6bd');

INSERT INTO user_items (id, user_id, item_id) VALUES ('e2b26306-5bda-484f-bb79-c19a2e96730c', '0bb54b71-b962-43e1-8209-e9ff8b79dbc1', 'deeb55e0-1463-4a42-998c-56fd43692788');
INSERT INTO user_items (id, user_id, item_id) VALUES ('ceb8f26f-50e5-442b-bafc-2422268c9272', '0bb54b71-b962-43e1-8209-e9ff8b79dbc1', '016437f9-2b33-4a6b-a4c1-7676e42d398c');

INSERT INTO user_perks (id, user_id, perk_id) VALUES ('f02b72f1-4af2-43c5-928b-a296d16b58fc', '0bb54b71-b962-43e1-8209-e9ff8b79dbc1', '016437f9-2b33-4a6b-a4c1-7676e42d398c');