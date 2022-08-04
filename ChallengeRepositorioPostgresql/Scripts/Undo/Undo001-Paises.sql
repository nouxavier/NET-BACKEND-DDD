
BEGIN;

	SELECT _v.unregister_patch('001-Paises');

	DROP TABLE paises;

COMMIT;