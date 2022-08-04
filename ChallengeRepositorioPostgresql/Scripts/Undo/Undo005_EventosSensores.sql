
BEGIN;

	SELECT _v.unregister_patch('005-EventosSensores');

	DROP TABLE eventos_sensores;

COMMIT;