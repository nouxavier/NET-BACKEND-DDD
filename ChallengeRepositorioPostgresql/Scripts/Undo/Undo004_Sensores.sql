
BEGIN;

	SELECT _v.unregister_patch('004-Sensores');

	DROP TABLE sensores;

COMMIT;