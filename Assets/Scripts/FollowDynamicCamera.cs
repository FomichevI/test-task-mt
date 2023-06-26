using System.Collections;
using UnityEngine;

public class FollowDynamicCamera : MonoBehaviour
{
    [SerializeField] private Transform _playerTrans;
    [SerializeField] private float _followSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private bool _raicingMod = false;
    [Header("Тряска камеры")]
    [SerializeField] private bool _shakeCamera = false;
    [SerializeField] private float _duration = 1;
    [SerializeField] private float _magnitude = 2;
    [SerializeField] private float _noize = 2;
    private Transform _pointTrans;
    private Vector3 _noizeOffset;
    private Vector3 _offset;
    private Quaternion _rotationOffset;
    private bool _isFallow = true;

    private void Start()
    {
        _offset = transform.position - _playerTrans.position;
        _rotationOffset = transform.rotation * _playerTrans.rotation;
        _noizeOffset = Vector3.zero;
        StartCoroutine(ShakeCameraCor(_duration, _magnitude, _noize));
    }

    private void FixedUpdate()
    {
        if (_isFallow)
        {
            transform.position = Vector3.Lerp(transform.position, _playerTrans.position + _offset + _noizeOffset,
               _followSpeed * Time.fixedDeltaTime);
            if (_raicingMod)
                transform.rotation = Quaternion.Lerp(transform.rotation, _playerTrans.rotation, _rotationSpeed * Time.fixedDeltaTime);
            else
                transform.rotation = Quaternion.Lerp(transform.rotation, _playerTrans.rotation * _rotationOffset, _rotationSpeed * Time.fixedDeltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, _pointTrans.position + _noizeOffset, _followSpeed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, _pointTrans.rotation, _rotationSpeed * Time.fixedDeltaTime);
        }
    }

    public void StartFallow()
    {
        _isFallow = true;
    }
    public void StopFallow(Transform newTrans)
    {
        _isFallow = false;
        _pointTrans = newTrans;
    }

    private IEnumerator ShakeCameraCor(float duration, float magnitude, float noize)
    {
        while (true)
        {
            //Инициализируем счётчиков прошедшего времени
            float elapsed = 0f;
            //Сохраняем стартовую локальную позицию
            Vector3 startPosition = transform.localPosition;
            //Генерируем две точки на "текстуре" шума Перлина
            Vector2 noizeStartPoint0 = Random.insideUnitCircle * noize;
            Vector2 noizeStartPoint1 = Random.insideUnitCircle * noize;

            //Выполняем код до тех пор пока не иссякнет время
            while (elapsed < duration)
            {
                //Генерируем две очередные координаты на текстуре Перлина в зависимости от прошедшего времени
                Vector2 currentNoizePoint0 = Vector2.Lerp(noizeStartPoint0, Vector2.zero, elapsed / duration);
                Vector2 currentNoizePoint1 = Vector2.Lerp(noizeStartPoint1, Vector2.zero, elapsed / duration);
                //Создаём новую дельту для камеры и умножаем её на длину дабы учесть желаемый разброс
                Vector2 cameraPostionDelta = new Vector2(Mathf.PerlinNoise(currentNoizePoint0.x, currentNoizePoint0.y), Mathf.PerlinNoise(currentNoizePoint1.x, currentNoizePoint1.y));
                cameraPostionDelta *= magnitude;

                //Перемещаем камеру в нувую координату
                _noizeOffset = (Vector3)cameraPostionDelta;

                //Увеличиваем счётчик прошедшего времени
                elapsed += Time.deltaTime;
                //Приостанавливаем выполнение корутины, в следующем кадре она продолжит выполнение с данной точки
                yield return null;
            }
        }
    }
}
