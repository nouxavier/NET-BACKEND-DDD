BEGIN;

	SELECT _v.register_patch('001-Paises', ARRAY['000-Config'], NULL);

	CREATE TABLE paises(
		id				UUID	NOT NULL,
		nome			CITEXT	NOT NULL,
		CONSTRAINT		perfil_pk
		PRIMARY KEY		(id),
		CONSTRAINT		perfil_nome_ak1
		UNIQUE			(nome)
	);

	insert into paises(id,nome)
	values('988fff3e-9458-4880-80fa-a3104c43dcbe','Brasil');

COMMIT;