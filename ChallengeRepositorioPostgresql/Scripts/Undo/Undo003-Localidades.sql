
BEGIN;

	SELECT _v.unregister_patch('003-Localidade');

	DROP TABLE localidades;

COMMIT;