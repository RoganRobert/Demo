using UnityEngine;

namespace Core
{
    public class AnimationHandler : IAnimationHandler
    {
        private Animator _animtor;

        public Animator GetAnimFromModel(Animator modelAnim)
        {
            return modelAnim;
        }

        public void GetRootAnim(Animator rootAnim)
        {
            _animtor = rootAnim;
        }


        public RuntimeAnimatorController GetAnimController(Animator modelAnim)
        {
            return modelAnim.runtimeAnimatorController;
        }

        public Avatar GetAnimAvatar(Animator modelAnim)
        {
            return modelAnim.avatar;
        }
        
        public void SetParam(AnimationParam param, string value)
        {
           
            string paramName = $"{param}";
            float flParsedValue;
            if (float.TryParse(value, out flParsedValue))
            {
                _animtor.SetFloat(paramName, flParsedValue);
                return;
            }
            
            bool boolParsedValue = false;
            if (bool.TryParse(value, out boolParsedValue))
            {
                _animtor.SetBool(paramName, boolParsedValue);
            }
        }

        
        public void SetParam(AnimationParam param)
        {
            string paramName = $"{param}";
            _animtor.SetTrigger(paramName);
        }
    }

    public enum AnimationParam
    {
        Speed,
        Jump
    }

    public interface IAnimationHandler
    {
        public Animator GetAnimFromModel(Animator modelAnim);
        public void GetRootAnim(Animator rootAnim);
        public void SetParam(AnimationParam param, string value);
        public void SetParam(AnimationParam param);
        public RuntimeAnimatorController GetAnimController(Animator modelAnim);
        public Avatar GetAnimAvatar(Animator modelAnim);

    }
}