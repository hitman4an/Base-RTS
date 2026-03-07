using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UnitAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetSpeed(float value)
    {
        _animator.SetFloat(CharacterAnimatorData.Params.Speed, value);
    }

    public class CharacterAnimatorData
    {
        public class Params
        {
            public static readonly int Speed = Animator.StringToHash(nameof(Speed));
        }
    }
}
