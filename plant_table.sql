--
-- PostgreSQL database dump
--

-- Dumped from database version 15.2
-- Dumped by pg_dump version 15.2

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: plant_table; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.plant_table (
    id bigint NOT NULL,
    object_id integer NOT NULL,
    cn_name text NOT NULL,
    latin_name text,
    common_name text,
    longitude double precision NOT NULL,
    latitude double precision NOT NULL,
    district text NOT NULL,
    street_town text NOT NULL,
    community text NOT NULL,
    road_name text NOT NULL,
    road_start text NOT NULL,
    road_end text NOT NULL,
    green_space_type text NOT NULL,
    located_in_road_direction text NOT NULL,
    age integer,
    age_determination_method text,
    chest_diameters real[] NOT NULL,
    height real NOT NULL,
    crown_spread_e_w real NOT NULL,
    crown_spread_s_n real NOT NULL,
    pool_shape text DEFAULT '无'::text NOT NULL,
    circle_cave text,
    square_length real,
    square_width real,
    pest_and_pathogen_damage text DEFAULT 5 NOT NULL,
    soil text NOT NULL,
    ground_condition text NOT NULL,
    growth text NOT NULL,
    root_stem_leaf_condition text NOT NULL,
    tilt real,
    divided_plants integer,
    conservation_status text NOT NULL,
    external_factors_affecting_growth text DEFAULT '无'::text NOT NULL,
    external_security_risks text DEFAULT '无'::text NOT NULL,
    protection_measures text DEFAULT '刷石灰'::text NOT NULL,
    remarks text,
    investigator text NOT NULL,
    investigation_time bigint NOT NULL,
    investigation_number text NOT NULL,
    listing_batch integer,
    plate_number text,
    tag_number2 text,
    taged boolean
);


ALTER TABLE public.plant_table OWNER TO postgres;

--
-- Data for Name: plant_table; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.plant_table (id, object_id, cn_name, latin_name, common_name, longitude, latitude, district, street_town, community, road_name, road_start, road_end, green_space_type, located_in_road_direction, age, age_determination_method, chest_diameters, height, crown_spread_e_w, crown_spread_s_n, pool_shape, circle_cave, square_length, square_width, pest_and_pathogen_damage, soil, ground_condition, growth, root_stem_leaf_condition, tilt, divided_plants, conservation_status, external_factors_affecting_growth, external_security_risks, protection_measures, remarks, investigator, investigation_time, investigation_number, listing_batch, plate_number, tag_number2, taged) FROM stdin;
\.


--
-- Name: plant_table plant_table_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.plant_table
    ADD CONSTRAINT plant_table_pkey PRIMARY KEY (id);


--
-- PostgreSQL database dump complete
--

