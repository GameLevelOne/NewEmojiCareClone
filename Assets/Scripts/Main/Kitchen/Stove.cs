using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stove : RoomItems {
	public List<Ingredient> ingredients = new List<Ingredient>();
	public GameObject buttonClear, cookTimerBar;
	public Cookbook cookbook;
	public Plate plate;
	public Table table;
	public PanelIngredient panelIngredient;

	public Image timerFiller;

	Recipe[] recipes;

	BoxCollider2D thisCollider;

	void Awake()
	{
		buttonClear.SetActive(false);
		cookTimerBar.SetActive(false);
		thisCollider = GetComponent<BoxCollider2D>();
		recipes = cookbook.recipes;
	}

	public void AddIngredient(Ingredient ingredient)
	{
		ingredients.Add(ingredient);
		if(ingredients.Count > 0) buttonClear.SetActive(true);

		ValidateRecipe();
	}

	void ValidateRecipe()
	{
		if(ingredients.Count == 4){
			CheckCombination(recipes[0].ingredients,0);
			CheckCombination(recipes[1].ingredients,1);
		}else if(ingredients.Count == 3){
			CheckCombination(recipes[2].ingredients,2);
			CheckCombination(recipes[3].ingredients,3);
			CheckCombination(recipes[4].ingredients,4);
		}
	}

	void CheckCombination(IngredientItems[] ingredients, int index)
	{
		int correctCounter = 0;
		for(int i = 0;i<ingredients.Length;i++){
			for(int j = 0;j < this.ingredients.Count;j++){
				if(ingredients[i] == this.ingredients[j].type) correctCounter++;
			}
		}
		
		if(correctCounter == ingredients.Length){
			StartCoroutine(Cook((float)recipes[index].cookDuration,index));
		}
	}

	IEnumerator Cook(float duration,int index)
	{
		table.ResetTable();
		panelIngredient.ResetButtons();
		thisCollider.enabled = false;
		cookTimerBar.SetActive(true);
		buttonClear.SetActive(false);
		float temp = 0;
		while(temp < duration){
			timerFiller.fillAmount = temp/duration;
			temp += Time.fixedDeltaTime;
			yield return new WaitForSeconds(Time.fixedDeltaTime);
		}

		cookTimerBar.SetActive(false);
		timerFiller.fillAmount = 0f;

		for(int i = 0;i<ingredients.Count;i++){
			Destroy(ingredients[i]);
		}
		ingredients.Clear();
		thisCollider.enabled = true;

		plate.Add(recipes[index].foodObject);
	}

	public void ButtonClearOnClick()
	{
		if(!editMode){
			ingredients.Clear();
			buttonClear.SetActive(false);
		}
	}
}