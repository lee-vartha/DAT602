const readlineSync = require('readline-sync');

let coins = 0;
const inventory = [];
const compliments = ['Alice', 'Bob', 'Charlie', 'David', 'Eve'];
const badWords = ['bad', 'mean', 'hate', 'stupid'];

const requiredIngredients = ['Flour', 'Sugar', 'Eggs', 'Butter', 'Milk', 'Vanilla Extract', 'Baking Powder'];
const ingredientCost = {
  'Flour': 2,
  'Sugar': 2,
  'Eggs': 3,
  'Butter': 2,
  'Milk': 3,
  'Vanilla Extract': 6,
  'Baking Powder': 6
};

function clearConsole() {
  console.clear();
}

function displayMenu() {
  console.log(`Coins: ${coins}\n`);
  console.log('1. Shopping List');
  console.log('2. Store');
  console.log('3. Inventory');
  console.log('');
  console.log('4. Compliment Time!!');
  console.log('5. Baker\'s Wish');
  console.log("-----------------");
  console.log('');
}

function store() {
  clearConsole();
  console.log('Welcome to the Store!');
  const ingredientToBuy = readlineSync.question('What ingredient do you want to buy? ');

  const ingredientCostValue = ingredientCost[ingredientToBuy] || 2;

  if (coins >= ingredientCostValue) {
    console.log(`Perfect! You have enough coins...`);
    coins -= ingredientCostValue;
    inventory.push(ingredientToBuy);
    console.log(`You bought ${ingredientToBuy}!`);
  } else {    
    console.log(`Sorry, you don't have enough coins. It's ${ingredientCostValue} coins. Compliment more people to earn coins!`);
  }

  readlineSync.question('Press Enter to continue...');
}

function inventoryMenu() {
  clearConsole();
  console.log('Inventory:');
  if (inventory.length === 0) {
    console.log('Your inventory is empty.');
  } else {
    console.log('Your inventory:');
    console.log(inventory.join(', '));
  }

  readlineSync.question('Press Enter to continue...');
}

function complimentTime() {
  clearConsole();
  console.log('Compliment Time!!');

  const randomPerson = compliments[Math.floor(Math.random() * compliments.length)];
  console.log(`Compliment ${randomPerson}!`);

  const compliment = readlineSync.question('Say something nice!! ');

  if (!badWords.some(word => compliment.toLowerCase().includes(word))) {
    console.log('Great compliment! You earned a coin!');
    coins++;
  } else {
    console.log('Oops! Be careful with your words.');
  }

  if (compliments.length > 3) {
    console.log('You can probably be more original and not repeat the same compliment.');
  }

  readlineSync.question('Press Enter to continue...');
}

function bakersWish() {
  clearConsole();
  console.log('Baker\'s Wish');

  if (inventory.length === 0) {
    console.log('Your inventory is empty. Compliment more people and buy ingredients to fulfill the Baker\'s Wish!');
  } else {
    console.log('Ingredients in your inventory:');
    console.log(inventory.join(', '));

    if (inventory.length === requiredIngredients.length && inventory.every(ingredient => requiredIngredients.includes(ingredient))) {
      console.log('Congratulations! You have all the ingredients. Time to bake the cake!');
    } else {
      console.log('You still need more ingredients to fulfill the Baker\'s Wish.');
    }
  }

  readlineSync.question('Press Enter to continue...');
}

function shoppingList() {
  clearConsole();
  console.log('Shopping List');

  console.log('\nRequired Ingredients:');
  requiredIngredients.forEach(ingredient => {
    const cost = ingredientCost[ingredient] || 2; // Default cost is 2 coins
    console.log(`- ${ingredient} (${cost} coins)`);
  });

  console.log('\nYour Inventory:');
  if (inventory.length === 0) {
    console.log('Your inventory is empty.');
  } else {
    inventory.forEach(ingredient => {
      const cost = ingredientCost[ingredient] || 2; // Default cost is 2 coins
      console.log(`- ${ingredient} (${cost} coins)`);
    });
  }

  const sellOption = readlineSync.keyInYNStrict('\nDo you want to sell excess ingredients?');

  if (sellOption) {
    sellIngredients();
  }
}

function sellIngredients() {
  console.log('Sell Ingredients');

  const ingredientToSell = readlineSync.question('Which ingredient do you want to sell? ');

  if (ingredientToSell === 'see price') {
    Object.keys(ingredientCost).forEach(ingredient => {
      console.log(`${ingredient}: ${ingredientCost[ingredient]} coins`);
    });
    return;
  }

  if (inventory.includes(ingredientToSell)) {
    const sellPrice = ingredientCost[ingredientToSell] || 2; // You get the ingredient cost back
    console.log(`You sold ${ingredientToSell} for ${sellPrice} coins.`);
    coins += sellPrice;

    // Remove the sold ingredient from the inventory
    const index = inventory.indexOf(ingredientToSell);
    inventory.splice(index, 1);
  } else {
    console.log('You don\'t have that ingredient in your inventory.');
  }
}

function gameLoop() {
  clearConsole();
  while (true) {
    displayMenu();

    const choice = readlineSync.questionInt('Welcome to Baker\'s Wish! What would you like to do? ');

    switch (choice) {
      case 1:
        shoppingList();
        break;
      case 2:
        store();
        break;
      case 3:
        inventoryMenu();
        break;
      case 4:
        complimentTime();
        break;
      case 5:
        bakersWish();
        break;
      default:
        console.log('Invalid choice. Please try again.');
        break;
    }
  }
}

// Start the game
gameLoop();
