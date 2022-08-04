
BEGIN;

	SELECT _v.unregister_patch('002-Regioes');

	DROP TABLE regioes;

COMMIT;