using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActiveSkillModule
{
   void OnCast(SkillExecutor executor, Skill_Info skill);
}
